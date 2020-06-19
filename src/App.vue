<template>
  <div id="app">
    <img src="./assets/logo.png" />
    <h1>Brain v1</h1>
    <div v-html="text" ></div>
    <searchBar @listUpdated="(newItem) => {items.push(newItem)}" />
    <item v-for="item in items" :key="item" :text="item" />
  </div>
</template>

<script>
import searchBar from './components/searchBar.vue'
import item from './components/item.vue'

export default {
  name: 'App',
  components: {
    searchBar,
    item
  },
  data: function () {
    return {
      items: [],
      text: ''
    }
  }
}
</script>