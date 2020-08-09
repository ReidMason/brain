<template>
  <div
    class="flex flex-col h-full bg-gray-300 border-r-2 border-gray-500"
    :style="`width: ${width}%;`"
    @click="$store.commit('setFocusedNote', note)"
  >
    <div class="flex bg-gray-500">
      <input
        class="w-full px-2 py-1 text-2xl bg-gray-500 focus:outline-none"
        v-model="note.name"
        :readonly="!editing"
      />
      <button
        class="mr-2 border-2 border-gray-600 bg-white focus:outline-none w-16"
        @click="updateNote(note.id, note)"
      >Save</button>
      <button
        class="mr-2 border-2 border-gray-600 bg-white focus:outline-none w-16"
        @click="editing = !editing"
      >{{ editing? 'View' : 'Edit' }}</button>
      <button
        class="border-2 border-gray-600 bg-white focus:outline-none w-16"
        @click="$store.state.selectedNotes.splice(index, 1);"
      >X</button>
    </div>
    <div class="content" style="background-color: #1e1e1e;">
      <div class="h-full" v-show="editing" @click="handleEditorContainerClick">
        <codemirror
          class="p-4 bg-nord-2 w-full h-full"
          ref="codemirror"
          v-model="note.content"
          :options="cmOptions"
          @input="checkForHints"
          @inputRead="handleInput"
          @keyHandled="handleKeys"
        />
      </div>
      <div class="p-4" v-show="!editing" v-html="note.content"></div>
    </div>

    <!-- Cursor dropdown -->
    <div v-show="suggestionListVisible">
      <div class="absolute z-10" :style="{left: `${caret.left}px`, top: `${caret.top + 20}px`}">
        <suggestion-list
          ref="suggestionList"
          :suggestions="tagSuggestions"
          @select-tag="replaceWord"
        />
      </div>
    </div>
  </div>
</template>

<script>
import NotesAPI from "../mixins/NotesAPI";
import { codemirror } from "vue-codemirror";

import "codemirror/lib/codemirror.css";

import "codemirror/addon/search/searchcursor";
import "codemirror/addon/search/search";

import SuggestionList from "./SuggestionList";

export default {
  props: {
    immutableNote: Object,
    index: Number,
  },
  components: {
    codemirror,
    SuggestionList,
  },
  mixins: [NotesAPI],
  data: function () {
    return {
      editing: true,
      note: this.immutableNote,
      suggestionListVisible: false,
      caret: {},
      editedWord: null,

      cmOptions: {
        mode: "text/x-markdown",
        lineNumbers: true,
        line: true,
        lineWiseCopyCut: true,
        extraKeys: {
          "Ctrl-Space": this.checkForHints,
          Down: this.parseDownArrow,
          Up: this.parseUpArrow,
          Enter: this.parseEnter,
        },
        events: ["beforeSelectionChange", "inputRead", "keyHandled"],
      },
      cm: null,
    };
  },
  methods: {
    handleInput(_, e) {
      // If input includes a space recalculate the tags
      if (e.text[0].includes(" ")) {
        let tags = this.note.content.match(/(#[^\s || #]{1,})/g);
        tags.forEach((tag) => {
          if (!this.$store.state.notes.allTags.includes(tag)) {
            this.$store.state.notes.allTags.push(tag);
          }
        });
      }
    },
    handleKeys(_, key) {
      if (key === "Esc") {
        this.suggestionListVisible = false;
      }
    },
    handleEditorContainerClick() {
      this.suggestionListVisible = false;
    },
    getEditedWord(cm) {
      console.log("Getting edited word");
      const cursor = cm.getCursor();
      const line = cm.getTokenAt(cursor, true).string;

      // Get the word being typed
      let start = 0;
      let end = 0;
      let word = "";

      let nextLetter = "";
      // Basically just go backwards from the cursor until you hit a space then you know the word ended
      for (let i = cursor.ch - 1; i >= 0; i--) {
        nextLetter = line.charAt(i);
        if (nextLetter == " ") {
          break;
        }
        word = nextLetter + word;
        start = i;
      }

      end = cursor.ch;
      // Now go forwards to add the end of the word
      for (let i = cursor.ch; i <= line.length; i++) {
        nextLetter = line.charAt(i);
        if (nextLetter == " ") {
          break;
        }
        word += nextLetter;
        end = i + 1;
      }

      return { start, end, word };
    },
    checkForHints() {
      let cm = this.$refs.codemirror.codemirror;
      let word = this.getEditedWord(cm);
      this.editedWord = word.word;

      this.suggestionListVisible = word.word.startsWith("#");
      if (this.suggestionListVisible) {
        this.caret = cm.cursorCoords(true);
      }
    },
    parseDownArrow() {
      if (this.suggestionListVisible) {
        this.$refs.suggestionList.moveDown();
      } else {
        return "CodeMirror.Pass";
      }
    },
    parseUpArrow() {
      if (this.suggestionListVisible) {
        this.$refs.suggestionList.moveUp();
      } else {
        return "CodeMirror.Pass";
      }
    },
    parseEnter(cm) {
      if (this.suggestionListVisible) {
        let replacement = this.$refs.suggestionList.suggestions[
          this.$refs.suggestionList.activeIndex
        ];
        this.replaceWord(replacement, cm);
      } else {
        return "CodeMirror.Pass";
      }
    },
    replaceWord(replacement, cm = null) {
      replacement += " ";
      cm = cm || this.$refs.codemirror.codemirror;
      let word = this.getEditedWord(cm);
      let cursor = cm.getCursor();

      // // Add a space to the replacement if there isn't already a space following it
      // const line = cm.getTokenAt(cursor, true).string;
      // let nextLetter = line.charAt(word.end);
      // if (nextLetter != " ") {
      //   replacement += " ";
      // }

      // Do the replacement
      cm.replaceRange(
        replacement,
        { line: cursor.line, ch: word.start },
        { line: cursor.line, ch: word.end }
      );
      cm.focus();
      cm.setCursor({ line: cursor.line, ch: word.start + replacement.length });
      this.suggestionListVisible = false;
    },
  },
  computed: {
    width: function () {
      return 100 / this.$store.state.selectedNotes.length;
    },
    tagSuggestions() {
      return this.$store.state.notes.allTags.filter((x) =>
        x.startsWith(this.editedWord)
      );
    },
  },
};
</script>

<style scoped>
.content {
  height: 96.5%;
}
</style>

<style>
.CodeMirror {
  @apply bg-nord-2;
  @apply text-nord-4;
  @apply h-full;
  @apply w-full;
  font: 400 15px Arial;
}

.CodeMirror-lines {
  @apply mt-2;
  @apply ml-2;
}

.CodeMirror-linenumber.CodeMirror-gutter-elt {
  @apply pl-0;
  @apply mr-2;
}

.CodeMirror-gutters {
  @apply bg-nord-1;
  @apply border-none;
}
</style>