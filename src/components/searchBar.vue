<template>
  <form @submit.prevent="$emit('listUpdated', fieldInput)">
    <input
      class="pl-2 py-1 block focus:outline-none w-full bg-nord-1 rounded text-white"
      type="text"
      placeholder="Search"
      @input="addSearchCriteria"
      v-model="fieldInput"
    />
  </form>
</template>

<script>
export default {
  name: "searchBar",
  data: function() {
    return {
      fieldInput: ""
    };
  },
  methods: {
    addSearchCriteria: function() {
      let rule = /(^|\s)(#[^\s]{1,})/g;

      if (this.fieldInput.match(rule)) {
        this.$store.commit("setTags", this.fieldInput.match(rule));
      }

      this.$store.commit("setSearchPhrase", this.fieldInput.replace(rule, ""));
    }
  }
};
</script>