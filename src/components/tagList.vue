<template>
    <div>
        <div class="flex">
            <div v-for="tag in tags" :key="tag" class="h-6 mx-2 flex bg-gray-500 rounded-full cursor-pointer">
                
                <div @click="removeTag()" class="pl-3 pr-2">X</div>
                <div @click="searchTag()" class="pr-3">{{tag}}</div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        tags: Array
    },
    methods: {
        removeTag: function() {
            console.log("remove")
        },
        searchTag: function() {
            console.log("search")
        }
    }
}
</script>