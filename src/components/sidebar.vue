<template>
  <div class="flex">
    <div class="w-16 h-screen bg-nord-1 border-r border-gray-900">
      <!-- Sections -->
      <div class="mx-auto px-2">
        <!-- Notes icon -->
        <div
          class="cursor-pointer mb-1"
          :class="this.displayedSection === 'notes' ? 'opacity-100' : 'opacity-50'"
          @click="setDisplayedSection('notes')"
        >
          <document-icon />
        </div>
        <!-- Search icon -->
        <div
          class="cursor-pointer"
          :class="this.displayedSection === 'search' ? 'opacity-100' : 'opacity-50'"
          @click="setDisplayedSection('search')"
        >
          <search-icon />
        </div>
      </div>

      <!-- Early access label -->
      <h1 class="fixed bottom-0 left-0">Brain Early Access Build :^)</h1>
    </div>
    <!-- Tab display -->
    <div class="bg-nord-0 w-64 border-r border-gray-900 border-opacity-50" v-if="displayedSection">
      <!-- File search -->

      <search-bar
        class="p-1 mb-3 border-b-2 border-gray-900 border-opacity-75"
        @listUpdated="(newItem) => { items.push(newItem); }"
      />
      <!-- Notes list -->
      <noteList
        class="bg-nord-0 text-nord-4 overflow-scroll overflow-y-auto overflow-x-hidden transition ease-in-out duration-700"
        v-if="displayedSection === 'notes'"
        @noteSelected="(node) => { console.log('test'); }"
      />

      <!-- Search results -->
      <div v-if="displayedSection === 'search'">
        <h1>Search</h1>
      </div>
    </div>
  </div>
</template>

<script>
import noteList from "./noteList";
import searchBar from "./searchBar";
import documentIcon from "../assets/icons/documentIcon";
import searchIcon from "../assets/icons/searchIcon";

export default {
  name: "sidebar",
  components: {
    noteList,
    searchBar,
    documentIcon,
    searchIcon
  },
  data: function() {
    return {
      displayedSection: "notes"
    };
  },
  methods: {
    setDisplayedSection: function(sectionName) {
      if (this.displayedSection === sectionName) {
        this.displayedSection = null;
      } else {
        this.displayedSection = sectionName;
      }
    }
  }
};
</script>