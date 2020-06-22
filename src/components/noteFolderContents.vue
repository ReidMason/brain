<template>
  <div>
    <!-- Folder title -->
    <div v-for="folder in folder.folders" :key="folder.id">
      <note-folder v-if="folder.folders" :folder="folder" @delete="removeFolder"></note-folder>
    </div>

    <!-- List of notes -->
    <div v-for="note in folder.notes" :key="note.id">
      <div @click="selectNote(note)">
        <noteItem :details="note" @delete="removeNote"></noteItem>
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
    },
    removeNote: function(note) {
      // Filters out the note that fired the delete event
      this.folder.notes = this.folder.notes.filter(n => {
        return n.id != note.id;
      });
    },
    removeFolder: function(folder) {
      // Filters out the folder that fired the delete event
      this.folder.folders = this.folder.folders.filter(n => {
        return n.id != folder.id;
      });
    }
  }
};
</script>
