<template>
  <div @click="$store.commit('setFocusedNote', note)">
    <div class="flex bg-gray-500">
      <input
        class="w-full px-2 py-1 text-2xl bg-gray-500 outline-none"
        v-model="note.name"
        :readonly="!editing"
      />
      <button class="mr-2 border-2 border-gray-600 bg-white outline-none w-16">Save</button>
      <button
        class="mr-2 border-2 border-gray-600 bg-white outline-none w-16"
        @click="editing = !editing"
      >{{ editing? 'View' : 'Edit' }}</button>
      <button
        class="border-2 border-gray-600 bg-white outline-none w-16"
        @click="$store.state.selectedNotes.splice(index, 1);"
      >X</button>
    </div>
    <div class="content">
      <textarea
        class="w-full h-full bg-gray-400 p-4 outline-none resize-none"
        v-if="editing"
        v-model="note.content"
      ></textarea>
      <div class="p-4" v-else v-html="note.content"></div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    immutableNote: Object,
    index: Number
  },
  data: function() {
    return {
      editing: true,
      note: this.immutableNote
    };
  }
};
</script>

<style scoped>
.content {
  height: 96.5%;
}
</style>
