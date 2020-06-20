<template>
  <div class="flex flex-col md:flex-row" id="app">
    <div class="flex flex-col w-full md:w-3/4 xl:w-4/5">
      <div class="pb-4 md:pb-0 bg-gray-700">
        <h1 class="fixed bottom-0 left-0 text-gray-100">Brain Early Access Build :^)</h1>
        <searchBar @listUpdated="(newItem) => {items.push(newItem);}" />
      </div>
      <editor class="bg-gray-300 h-full" :immutableNote="selectedNote" />
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
    ...mapState(["tags", "searchPhrase"])
  }
};
</script>
