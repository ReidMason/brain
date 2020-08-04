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
    details: Object,
  },
  data: function () {
    return {
      beingDragged: false,
    };
  },
  computed: {
    isFocused: function () {
      return (
        this.$store.state.focusedNote &&
        this.details.id === this.$store.state.focusedNote.id
      );
    },
  },
  methods: {
    dragStart: function (e) {
      // The item drag is started so add the current item to the moving element store
      this.$store.commit("setMovingElement", this.details);
      // Remove drag and drop ghost image
      e.dataTransfer.setDragImage(new Image(), 0, 0);
      this.beingDragged = true;
    },
    dragend: function () {
      // The item has been released
      this.beingDragged = false;
    },
  },
};
</script>
