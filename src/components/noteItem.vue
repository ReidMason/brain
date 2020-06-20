<template>
  <div>
    <h3>{{ details.title }}</h3>
    <div>{{ details.content }}</div>
  </div>
</template>

<script>
  export default {
    props: {
        details: Object,
    },
    data: function () {
      return {
      }
    }
  }
</script>