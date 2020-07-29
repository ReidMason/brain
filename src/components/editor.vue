<template>
  <div
    class="flex flex-col h-full bg-gray-300 border-r-2 border-gray-500"
    :style="`width: ${width}%;`"
    @click="$store.commit('setFocusedNote', note)"
  >
    <div class="flex bg-gray-500">
      <input
        class="w-full px-2 py-1 text-2xl bg-gray-500 focus:outline-none"
        v-model="note.name"
        :readonly="!editing"
      />
      <button
        class="mr-2 border-2 border-gray-600 bg-white focus:outline-none w-16"
        @click="$store.dispatch('save')"
      >Save</button>
      <button
        class="mr-2 border-2 border-gray-600 bg-white focus:outline-none w-16"
        @click="editing = !editing"
      >{{ editing? 'View' : 'Edit' }}</button>
      <button
        class="border-2 border-gray-600 bg-white focus:outline-none w-16"
        @click="$store.state.selectedNotes.splice(index, 1);"
      >X</button>
    </div>
    <div class="content" style="background-color: #1e1e1e;">
      <div class="p-4" v-show="!editing" v-html="note.content"></div>
    </div>
    <tagList :tags="note.tags" />
  </div>
</template>

<script>
import tagList from './tagList';

export default {
  components: {
    tagList
  },
  props: {
    immutableNote: Object,
    index: Number
  },
  data: function() {
    return {
      editing: true,
      options: {},
      note: this.immutableNote
    };
  },
  computed: {
    width: function() {
      return 100 / this.$store.state.selectedNotes.length;
    }
  }
};
</script>

<style scoped>
.content {
  height: 96.5%;
}
</style>
