<template>
  <ul>
    <li v-for="(suggestion, i) in suggestions" :key="i">
      <div
        class="cursor-pointer bg-nord-0 p-1 pl-2 hover:bg-nord-10"
        :class="{'bg-nord-10' : i === activeIndex}"
        @click="$emit('select-tag', suggestions[i])"
      >{{suggestion}}</div>
    </li>
  </ul>
</template>

<script>
import utils from "../mixins/Utils";

export default {
  name: "SuggestionList",
  data: function () {
    return {
      activeIndex: 0,
    };
  },
  mixins: [utils],
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
      return this.getAllTags();
    },
  },
};
</script>

<style scoped>
li {
  min-width: 10em;
}
</style>