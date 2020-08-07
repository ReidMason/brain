<template>
  <div class="flex flex-wrap mt-1 pt-1 pr-1">
    <div
      v-for="(tag, index) in tags"
      :key="index"
      class="h-6 ml-1 mb-1 flex bg-gray-600 text-nord-0 rounded-full overflow-hidden"
    >
      <div @click="$emit('remove-tag', index)" class="pl-1 pr-1 pt-1 cursor-pointer hover:text-nord-9"><cross-icon /></div>
      <div class="pr-3 leading-snug truncate">{{tag}}</div>
    </div>
  </div>
</template>

<script>
import crossIcon from "../assets/icons/crossIcon"

export default {
  props: {
    tags: Array,
  },
  components: {
    crossIcon
  },
};
</script>