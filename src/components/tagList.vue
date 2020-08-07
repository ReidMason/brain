<template>
  <div class="flex flex-wrap mt-1 pt-1 pr-1">
    <div
      v-for="tag in tags"
      :key="tag.id"
      class="h-6 ml-1 mb-1 flex bg-gray-500 rounded-full cursor-pointer overflow-hidden"
    >
      <!-- Cross icon still not displaying, svg has no height or width -->
      <div @click="removeTag()" class="pl-3 pr-2 text-md">
        <cross-icon />
      </div>
      <div class="pr-3 leading-snug truncate">{{tag.tag}}</div>
    </div>
  </div>
</template>

<script>
import crossIcon from "../assets/icons/crossIcon";

export default {
  props: {
    tags: Array,
  },
  components: {
    crossIcon,
  },
  methods: {},
};
</script>