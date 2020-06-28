<template>
  <div class="overflow-scroll overflow-y-auto overflow-x-hidden">
    <!-- Folder name -->
    <div
      class="flex"
      @click="expanded = !expanded"
      @drop="drop"
      @dragenter.prevent="dragenter"
      @dragleave.prevent="dragleave"
      @dragover.prevent
    >
      <!-- Expand and collapse icons -->
      <div class="w-5 my-auto">
        <chevron-right-icon v-if="expanded" />
        <chevron-down-icon v-else />
      </div>
      <!-- Folder name -->
      <noteItem :class="{'bg-nord-9': hovered}" :details="folder" @delete="remove"></noteItem>
    </div>

    <!-- Folder contents both notes and nested folders -->
    <noteFolderContents :folder="folder" v-if="expanded"></noteFolderContents>
  </div>
</template>

<script>
import noteItem from "./noteItem";
import chevronRightIcon from "./icons/chevronRightIcon";
import chevronDownIcon from "./icons/chevronDownIcon";

export default {
  name: "noteFolder",
  props: {
    folder: Object,
    startExpanded: Boolean
  },
  components: {
    noteItem,
    chevronRightIcon,
    chevronDownIcon
  },
  data: function() {
    return {
      expanded: this.startExpanded,
      hovered: false,
      dragover: false
    };
  },
  beforeCreate: function() {
    this.$options.components.noteFolderContents = require("./noteFolderContents").default;
  },
  methods: {
    drop: function() {
      // Triggered when the folder has an item dropped onto it
      // This item can either be a note or another folder

      // Element is already in this folders list of folders
      var folderAlreadyInFolders = this.folder.folders.includes(
        this.$store.getters.movingElement
      );
      // Element is already in this folders list of notes
      var noteAlreadyInNotes = this.folder.notes.includes(
        this.$store.getters.movingElement
      );
      // Element is being dropped onto itself
      var elementIsItself = this.folder === this.$store.getters.movingElement;

      // If nether are true the element should be moved as this is a new location
      if (!(folderAlreadyInFolders || noteAlreadyInNotes || elementIsItself)) {
        // Add it to folders if it's a folder and add it to notes if it's a note
        if (this.$store.getters.movingElement.folders != null) {
          this.folder.folders.push(this.$store.getters.movingElement);
        } else {
          this.folder.notes.push(this.$store.getters.movingElement);
        }
        // Move was susccessful so set the movingElement to null so the element knows to emit the delete event
        this.$store.commit("setMovingElement", true);
        // Expand the folder to show it was moved
        this.expanded = true;
      } else {
        // Move was unsuccessful
        this.$store.commit("setMovingElement", false);
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
