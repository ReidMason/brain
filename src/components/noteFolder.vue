<template>
  <div class="overflow-scroll overflow-y-auto overflow-x-hidden">
    <!-- Folder name -->
    <div class="cursor-pointer pl-2 hover:bg-gray-900" :class="{ 'bg-green-800': expanded }" @click="expanded = !expanded">
      <noteItem :details="folder"></noteItem>
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
      expanded: true
    };
  },
  beforeCreate: function() {
    this.$options.components.noteFolderContents = require("./noteFolderContents").default;
  }
};
</script>
