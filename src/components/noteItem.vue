<template>
  <div class="hover:bg-gray-900 cursor-pointer py-4 pl-4">
    <h3>{{ details.name }}</h3>
  </div>
</template>

<script>
export default {
  props: {
    details: Object,
  },
  data: function() {
    return {};
  },
};
</script>
