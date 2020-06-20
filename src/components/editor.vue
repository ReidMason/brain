<template>
  <div>
    <div v-if="editing" contenteditable="true" @input="$emit('edited', note)"></div>
    <div v-else></div>
    <button @click="editing = !editing">Toggle</button>
  </div>
</template>

<script>
export default {
  props: {
    immutableNote: Object
  },
  data: function() {
    return {
      editing: true,
      note: this.immutableNote
    };
  }
};
</script>

<style scoped>
div {
  height: 100%;
}
</style>
