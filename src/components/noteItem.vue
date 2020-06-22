<template>
  <div
    class="cursor-pointer pl-2 hover:bg-gray-900"
    draggable="true"
    @dragstart="dragStart"
    @dragend="dragend"
  >
    <h3>{{ details.name }}</h3>
  </div>
</template>

<script>
export default {
  props: {
    details: Object
  },
  methods: {
    dragStart: function() {
      // The item drag is started so add the current item to the moving element store
      this.$store.commit("setMovingElement", this.details);
    },
    dragend: function() {
      // The item has been released so we need to remove it if it has been moved to a different folder
      // The movingElement store value will be set to null if the element was moved
      // So only emit the delete event if the store value is null
      if (this.$store.getters.movingElement === null) {
        this.$emit("delete", this.details);
      }
    }
  }
};
</script>
