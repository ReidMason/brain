<template>
  <div
    class="cursor-pointer w-full"
    :class="{'bg-nord-10': beingDragged, 'hover:bg-nord-3': !beingDragged, 'bg-nord-3': isFocused}"
    draggable="true"
    @dragstart="dragStart"
    @dragend="dragend"
  >
    <header>{{ details.name }}</header>
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
  computed: {
    isFocused: function() {
      return (
        this.$store.state.focusedNote &&
        this.details.id === this.$store.state.focusedNote.id
      );
    }
  },
  methods: {
    dragStart: function(e) {
      // The item drag is started so add the current item to the moving element store
      this.$store.commit("setMovingElement", this.details);
      // Remove drag and drop ghost image
      e.dataTransfer.setDragImage(new Image(), 0, 0);
      this.beingDragged = true;
    },
    dragend: function() {
      // The item has been released so we need to remove it if it has been moved to a different folder
      // The movingElement store value will be set to true if the element was moved
      // So only emit the delete event if the store value is true
      if (this.$store.getters.movingElement === true) {
        this.$emit("delete", this.details);
      }
      this.$store.commit("setMovingElement", null);
      this.beingDragged = false;
    }
  }
};
</script>
