<template>
  <div class="overflow-scroll overflow-y-auto overflow-x-hidden">
    <!-- Folder name -->
    <div
      class="cursor-pointer pl-2 hover:bg-gray-900"
      :class="{ 'bg-green-800': expanded, 'text-red-700' : hovered }"
      @click="expanded = !expanded"
      @drop="drop"
      @dragenter.prevent="dragenter"
      @dragleave.prevent="dragleave"
      @dragover.prevent
    >
      <noteItem :details="folder" @delete="remove"></noteItem>
    </div>

    <!-- Folder contents both notes and nested folders -->
    <noteFolderContents class="ml-4" :folder="folder" v-if="expanded"></noteFolderContents>
  </div>
</template>

<script>
import noteItem from "./noteItem";

export default {
  name: "noteFolder",
  props: {
    folder: Object
  },
  components: {
    noteItem
  },
  data: function() {
    return {
      expanded: true,
      hovered: false
    };
  },
  beforeCreate: function() {
    this.$options.components.noteFolderContents = require("./noteFolderContents").default;
  },
  methods: {
    drop: function() {
      // Triggered when the folder has an item dropped onto it
      // This item can either be a note or another folder

      // First check if the element is already in this folders list of folders
      var folderAlreadyInFolders = this.folder.folders.includes(
        this.$store.getters.movingElement
      );
      // Then check if the element is already in this folders list of notes
      var noteAlreadyInNotes = this.folder.notes.includes(
        this.$store.getters.movingElement
      );
      // If nether are true the element should be moved as this is a new location
      if (!(folderAlreadyInFolders || noteAlreadyInNotes)) {
        // Add it to folders if it's a folder and add it to notes if it's a note
        if (this.$store.getters.movingElement.folders != null) {
          this.folder.folders.push(this.$store.getters.movingElement);
        } else {
          this.folder.notes.push(this.$store.getters.movingElement);
        }
        // Move was susccessful so set the movingElement to null so the element knows to emit the delete event
        this.$store.commit("setMovingElement", null);
      }

      // Regardless of anything being moved we need to act as if the drag has left the element to remove the highlight
      this.dragleave();
    },
    dragenter: function() {
      this.hovered = true;
    },
    dragleave: function() {
      this.hovered = false;
    },
    remove: function(folder) {
      this.$emit("delete", folder);
    }
  }
};
</script>
