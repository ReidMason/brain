<template>
  <div class="cursor-pointer pl-2 hover:bg-gray-900">
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
