<template>
  <form @submit.prevent="$emit('listUpdated', fieldInput)">
    <input class="block w-11/12 md:w-64 mx-auto m-3 md:m-2 pl-2 border-2 border-gray-300" type="text" name="prime" placeholder="SEARCH" @input="addSearchCriteria" v-model="fieldInput" />
  </form>
</template>

<script>
export default {
  data: function() {
    return {
      fieldInput: "",
    };
  },
  methods: {
    addSearchCriteria: function() {
      let rule = /(^|\s)(#[^\s]{1,})/g;

      if (this.fieldInput.match(rule)) {

        this.$store.commit('setTags', this.fieldInput.match(rule))

      }

      this.$store.commit('setSearchPhrase', this.fieldInput.replace(rule, ''))

    }
  }
};
</script>