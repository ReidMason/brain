<template>
  <ul>
    <li v-for="(suggestion, i) in suggestions" :key="i">
      <div
        class="cursor-pointer bg-nord-0 p-1 pl-2"
        :class="{'bg-nord-10' : i === activeIndex}"
      >{{suggestion}}</div>
    </li>
  </ul>
</template>

<script>
export default {
  name: "SuggestionList",
  data: function () {
    return {
      activeIndex: 0,
    };
  },
  methods: {
    moveDown: function () {
      if (this.activeIndex < this.suggestions.length - 1) {
        this.activeIndex++;
      }
    },
    moveUp: function () {
      if (this.activeIndex > 0) {
        this.activeIndex--;
      }
    },
  },
  computed: {
    suggestions: function () {
      return ["Sug1", "Sug2", "Sug3", "Sug4", "Sug5"];
    },
  },
};
</script>

<style scoped>
li {
  min-width: 10em;
}
</style>