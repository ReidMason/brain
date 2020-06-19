<template>
  <form @submit.prevent="$emit('listUpdated', fieldInput)" >
    <label for="prime">Search</label>
    <input type="text" name="prime" v-model="fieldInput">
    <button type="submit" >Submit</button>
  </form>
</template>

<script>
  export default {
    data: function () {
      return {
        fieldInput: ''
      }
    }
  }
</script>