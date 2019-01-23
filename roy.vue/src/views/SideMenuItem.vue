<template>
    <Submenu :name="parentItem.name">
        <template slot="title">
            <Icon :type = "parentItem.icon" :size="15"/>
            <span>{{ parentItem.moduleName }}</span>
        </template>
        <template v-for="item in parentItem.childrenList">
            <side-menu-item 
                v-if = "item.childrenList && item.childrenList.length!==0" 
                :parent-item = "item" 
                :key = "item.moduleId"
            >
            </side-menu-item>
            <menu-item v-else :key = "item.moduleId" :name = "item.moduleName">
                <span>{{ item.moduleName }}</span>
            </menu-item>
        </template>
    </Submenu>
</template>

<script>
export default {
    name: 'sideMenuItem',
    props: {
        parentItem: {
            type: Object,
              default: () => {}
        }
    }
}
</script>

<style lang="scss" scoped>

</style>