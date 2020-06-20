<template>
  <form @submit.prevent="$emit('listUpdated', fieldInput)">
    <label for="prime">Search</label>
    <input type="text" name="prime" @input="addSearchCriteria" v-model="fieldInput" />
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