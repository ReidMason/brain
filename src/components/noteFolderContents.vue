<template>
  <div>
    <!-- Folder title -->
    <div v-for="folder in folder.folders" :key="folder.id">
      <note-folder v-if="folder.folders" :folder="folder"></note-folder>
    </div>

    <!-- List of notes -->
    <ul v-for="note in folder.notes" :key="note.id">
      <noteItem class="ml-4" :details="note"></noteItem>
    </ul>
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
  }
};
</script>
