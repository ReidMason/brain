<template>
  <div id="app">
    <img src="./assets/logo.png" />
    <h1>Brain v0.1</h1>
    <div>{{ searchPhrase }}</div>
    <div v-for="tag in tags" :key="tag">{{ tag }}</div>
    <searchBar @listUpdated="(newItem) => {items.push(newItem)}" />
    <noteList />
  </div>
</template>

<script>
import searchBar from './components/searchBar.vue'
import noteList from './components/noteList.vue'
import { mapState } from 'vuex'

export default {
  name: 'App',
  components: {
    searchBar,
    noteList
  },
  data: function () {
    return {
      items: [],
    }
  },
  computed: {
      ...mapState(['tags', 'searchPhrase'])
  }
}
</script>