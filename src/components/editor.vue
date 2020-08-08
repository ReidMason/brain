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
      <div class="h-full" v-show="editing">
        <codemirror
          class="p-4 bg-nord-2 w-full h-full"
          ref="codemirror"
          v-model="note.content"
          :options="cmOptions"
          @input="checkForHints"
        />
      </div>
      <div class="p-4" v-show="!editing" v-html="note.content"></div>
    </div>

    <!-- Cursor dropdown -->
    <div v-if="suggestionListVisible">
      <div class="absolute" :style="{left: `${caret.left}px`, top: `${caret.top}px`}">
        <suggestion-list ref="suggestionList" />
      </div>
    </div>
  </div>
</template>

<script>
import NotesAPI from "../mixins/NotesAPI";
import { codemirror } from "vue-codemirror";

import Utils from "../mixins/Utils";
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
  mixins: [NotesAPI, Utils],
  data: function () {
    return {
      editing: true,
      note: this.immutableNote,
      suggestionListVisible: false,
      caret: {},

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
        events: ["beforeSelectionChange"],
      },
      cm: null,
    };
  },
  methods: {
    getEditedWord(cm) {
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

      // Now go forwards to add the end of the word
      for (let i = cursor.ch; i <= line.length; i++) {
        nextLetter = line.charAt(i);
        if (nextLetter == " ") {
          break;
        }
        word += nextLetter;
        end = i;
      }

      return { start, end, word };
    },
    checkForHints() {
      let cm = this.$refs.codemirror.codemirror;
      let word = this.getEditedWord(cm);

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
        let replacement =
          this.$refs.suggestionList.suggestions[
            this.$refs.suggestionList.activeIndex
          ] + " ";
        let word = this.getEditedWord(cm);
        let cursor = cm.getCursor();
        cm.replaceRange(
          replacement,
          { line: cursor.line, ch: word.start },
          { line: cursor.line, ch: word.end }
        );
        this.suggestionListVisible = false;
      } else {
        return "CodeMirror.Pass";
      }
    },
  },
  computed: {
    width: function () {
      return 100 / this.$store.state.selectedNotes.length;
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