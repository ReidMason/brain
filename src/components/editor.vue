<template>
  <div
    class="flex flex-col h-full w-full bg-gray-300 border-r-2 border-gray-500"
    @click="$store.commit('setFocusedNote', note)"
  >
    <div class="flex bg-gray-500">
      <input
        class="w-full px-2 py-1 text-2xl bg-gray-500 focus:outline-none"
        v-model="note.name"
        :readonly="!editing"
      />
      <button class="mr-2 border-2 border-gray-600 bg-white focus:outline-none w-16">Save</button>
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
      <div class="w-full h-full" id="container" v-if="editing" language="javascript" />
      <div class="p-4" v-else v-html="note.content"></div>
    </div>
  </div>
</template>

<script>
import * as monaco from "monaco-editor";

export default {
  props: {
    immutableNote: Object,
    index: Number
  },
  data: function() {
    return {
      editing: true,
      options: {},
      note: this.immutableNote
    };
  },
  mounted() {
    monaco.editor.create(document.getElementById("container"), {
      value: this.note.content,
      language: "html",
      theme: "vs-dark"
    });
  }
};
</script>

<style scoped>
.content {
  height: 96.5%;
}
</style>
