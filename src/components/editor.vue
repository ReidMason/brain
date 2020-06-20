<template>
  <div>
    <div v-if="editing" contenteditable="true"></div>
    <div v-else></div>
    <button @click="editing = !editing">Toggle</button>
  </div>
</template>

<script>
export default {
  data: function() {
    return {
      editing: true,
    };
  },
};
</script>

<style scoped>
    div {
        height: 100%;
    }
</style>