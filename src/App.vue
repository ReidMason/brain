<template>
  <div id="app">
    <div class="flex flex-col md:flex-row">
      <div class="flex flex-col w-full md:w-3/4 xl:w-4/5">
        <div class="pb-4 md:pb-0 bg-gray-700">
          <h1 class="fixed bottom-0 left-0 text-gray-100">Brain Early Access Build :^)</h1>
          <searchBar @listUpdated="(newItem) => {items.push(newItem);}" />
        </div>
        <div class="h-full flex">
          <editor
            class="bg-gray-300 h-full w-full border-r-2 border-gray-600"
            v-for="note in this.$store.getters.selectedNotes"
            :key="note.id"
            :immutableNote="note"
          />
        </div>
        <!-- <div>
          <div>{{ searchPhrase }}</div>
          <div v-for="tag in tags" :key="tag" >{{ tag }}</div>
        </div>-->
      </div>

      <noteList
        class="md:order-first h-screen w-full md:w-1/4 xl:w-1/5 overflow-scroll overflow-y-auto overflow-x-hidden bg-gray-800 text-gray-100"
        @noteSelected="(node) => { console.log('test')}"
      />
    </div>
    <div class="bg-gray-500 w-full px-2">
      <div v-if="focusedNote" class="flex flex-row-reverse">
        <span class="ml-4">Words: {{ focusedNote.content.split(' ').length }}</span>
        <span>Characters: {{ focusedNote.content.length}}</span>
      </div>
    </div>
  </div>
</template>

<script>
import searchBar from "./components/searchBar.vue";
import noteList from "./components/noteList.vue";
import editor from "./components/editor.vue";
import { mapState } from "vuex";

export default {
  name: "App",
  components: {
    searchBar,
    noteList,
    editor
  },
  data: function() {
    return {
      items: [],
      selectedNote: null
    };
  },
  computed: {
    ...mapState(["tags", "searchPhrase", "focusedNote"])
  }
};
</script>