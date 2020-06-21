<template>
  <div>
    <!-- Folder title -->
    <div v-for="folder in folder.folders" :key="folder.id">
      <note-folder v-if="folder.folders" :folder="folder"></note-folder>
    </div>

    <!-- List of notes -->
    <div v-for="note in folder.notes" :key="note.id">
      <div @click="selectNote(note)">
        <noteItem :details="note"></noteItem>
      </div>
    </div>
  </div>
</template>

<script>
import noteFolder from "./noteFolder";
import noteItem from "./noteItem";

export default {
  name: "noteFolderContents",
  components: {
    noteFolder,
    noteItem
  },
  props: {
    folder: Object
  },
  methods: {
    selectNote: function(note) {
      this.$store.commit("addToSelectedNotes", note);
    }
  }
};
</script>
