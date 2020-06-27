<template>
  <div class="flex">
    <div class="w-16 h-screen bg-gray-800 border-r-2 border-gray-900">
      <!-- Sections -->
      <div class="mx-auto px-2 text-gray-600 text-opacity-75">
        <!-- Notes icon -->
        <div
          :class="{'text-gray-400' : this.displayedSection === 'notes'}"
          @click="setDisplayedSection('notes')"
        >
          <svg
            fill="currentColor"
            viewBox="0 0 20 20"
            data-darkreader-inline-fill
            style="--darkreader-inline-fill:currentColor;"
          >
            <path
              fill-rule="evenodd"
              d="M4 4a2 2 0 012-2h4.586A2 2 0 0112 2.586L15.414 6A2 2 0 0116 7.414V16a2 2 0 01-2 2H6a2 2 0 01-2-2V4z"
              clip-rule="evenodd"
            />
          </svg>
        </div>
        <!-- Search icon -->
        <div :class="{'text-gray-400' : this.displayedSection === 'search'}">
          <svg
            @click="setDisplayedSection('search')"
            fill="currentColor"
            viewBox="0 0 20 20"
            data-darkreader-inline-fill
            style="--darkreader-inline-fill:currentColor;"
          >
            <path
              fill-rule="evenodd"
              d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
              clip-rule="evenodd"
            />
          </svg>
        </div>
      </div>

      <!-- Early access label -->
      <h1 class="fixed bottom-0 left-0 text-gray-100">Brain Early Access Build :^)</h1>
    </div>
    <!-- Tab display -->
    <div class="bg-gray-700 w-64" v-if="displayedSection">
      <!-- File search -->
      <search-bar
        class="p-1 border-b-2 border-black"
        @listUpdated="(newItem) => { items.push(newItem); }"
      />

      <!-- Notes list -->
      <noteList
        v-if="displayedSection === 'notes'"
        class="bg-gray-800 text-gray-100 overflow-scroll overflow-y-auto overflow-x-hidden"
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

export default {
  name: "sidebar",
  components: {
    noteList,
    searchBar
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