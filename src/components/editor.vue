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
        <textarea
          ref="editWindow"
          class="p-4 bg-nord-2 w-full h-full"
          v-model="note.content"
          @input="checkSuggestions"
          @keyup="checkKeypress"
        ></textarea>
      </div>
      <div class="p-4" v-show="!editing" v-html="note.content"></div>
    </div>

    <!-- Cursor dropdown -->
    <div v-if="dropdownVisible">
      <div class="absolute" :style="{left: `${caret.left + 320}px`, top: `${caret.top + 70}px`}">
        <suggestion-list ref="suggestionList" @select-tag="inputSuggestion" />
      </div>
    </div>
  </div>
</template>

<script>
import NotesAPI from "../mixins/NotesAPI";
import getCaretCoordinates from "../../textarea-caret-position";
import SuggestionList from "./SuggestionList";

export default {
  props: {
    immutableNote: Object,
    index: Number,
  },
  components: {
    SuggestionList,
  },
  mixins: [NotesAPI],
  data: function () {
    return {
      editing: true,
      options: {},
      note: this.immutableNote,

      caret: null,
      dropdownVisible: false,
    };
  },
  methods: {
    checkKeypress: function (e) {
      let ele = this.$refs.editWindow;
      let position = ele.selectionEnd;

      if (e.code === "Space" && e.ctrlKey) {
        this.checkSuggestions();
      } else if (e.code === "ArrowLeft" || e.code === "ArrowRight") {
        this.dropdownVisible = false;
      } else if (e.code === "ArrowDown" && this.dropdownVisible) {
        e.preventDefault();
        this.$refs.suggestionList.moveDown();
      } else if (e.code === "ArrowUp" && this.dropdownVisible) {
        e.preventDefault();
        this.$refs.suggestionList.moveUp();
      } else if (e.code === "Enter" && this.dropdownVisible) {
        let selection = this.$refs.suggestionList.suggestions[
          this.$refs.suggestionList.activeIndex
        ];
        this.inputSuggestion(selection);
        this.dropdownVisible = false;
      }

      ele.setSelectionRange(position, position);
    },
    inputSuggestion: function (selection) {
      let ele = this.$refs.editWindow;
      let firstLetter = 0;
      let lastLetter = 0;

      // Get the word being typed
      let nextLetter = "";
      // Basically just go backwards from the cursor until you hit a space then you know the word ended
      for (let i = ele.selectionEnd - 1; i >= 0; i--) {
        nextLetter = this.note.content.charAt(i);
        if (nextLetter == " ") {
          break;
        }
        firstLetter = i;
      }

      // Now go forwards to add the end of the word
      for (let i = ele.selectionEnd; i <= this.note.content.length; i++) {
        nextLetter = this.note.content.charAt(i);
        if (nextLetter == " ") {
          break;
        }
        lastLetter = i + 2;
      }

      String.prototype.replaceBetween = function (start, end, what) {
        return this.substring(0, start) + what + this.substring(end);
      };

      this.note.content = this.note.content.replaceBetween(
        firstLetter,
        lastLetter,
        selection + " "
      );

      this.dropdownVisible = false;
    },
    checkSuggestions: function () {
      let ele = this.$refs.editWindow;

      // Get the word being typed
      let editingWord = "";
      let nextLetter = "";
      // Basically just go backwards from the cursor until you hit a space then you know the word ended
      for (let i = ele.selectionEnd - 1; i >= 0; i--) {
        nextLetter = this.note.content.charAt(i);
        if (nextLetter == " ") {
          break;
        }
        editingWord = nextLetter + editingWord;
      }

      // Now go forwards to add the end of the word
      for (let i = ele.selectionEnd; i <= this.note.content.length; i++) {
        nextLetter = this.note.content.charAt(i);
        if (nextLetter == " ") {
          break;
        }
        editingWord += nextLetter;
      }

      // Find the cursor location
      if (editingWord.length > 0 && editingWord[0].startsWith("#")) {
        var caret = getCaretCoordinates(ele, ele.selectionEnd);
        this.caret = caret;
        this.dropdownVisible = true;
      } else {
        this.dropdownVisible = false;
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
