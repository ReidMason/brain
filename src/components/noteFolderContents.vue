<template>
  <div class="pl-2">
    <!-- Folder title -->
    <div v-for="folder in folder.folders" :key="folder.id">
      <note-folder v-if="folder.folders" :folder="folder" @delete="removeFolder"></note-folder>
    </div>

    <!-- List of notes -->
    <div v-for="note in folder.notes" :key="note.id">
      <div class="flex" @click="selectNote(note)">
        <!-- Spacer div to keep the notes inline with the folders -->
        <div class="w-5"></div>
        <note-item :deta-ils="note" @delete="removeNote"></note-item>
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
