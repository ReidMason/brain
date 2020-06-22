<template>
  <div
    class="cursor-pointer pl-2 hover:bg-gray-900"
    :style="'background-color: ' + (beingDragged ? 'blue' : '')"
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
  data: function() {
    return {
      beingDragged: false
    };
  },
  methods: {
    dragStart: function(e) {
      // The item drag is started so add the current item to the moving element store
      this.$store.commit("setMovingElement", this.details);
      // Remove drag and drop ghost image
      e.dataTransfer.setDragImage(new Image(), 0, 0);
      e.dataTransfer.effectAllowed = "move";
      this.beingDragged = true;
    },
    dragend: function() {
      // The item has been released so we need to remove it if it has been moved to a different folder
      // The movingElement store value will be set to null if the element was moved
      // So only emit the delete event if the store value is null
      if (this.$store.getters.movingElement === null) {
        this.$emit("delete", this.details);
      }
      this.beingDragged = false;
    }
  }
};
</script>
