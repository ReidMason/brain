<template>
  <div class="hover:bg-gray-900 cursor-pointer py-4 pl-4">
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