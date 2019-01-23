<template>
    <Menu mode="horizontal"  active-name="1">
        <template v-for="item in menuData">
            <side-menu-item v-if="item.childrenList && item.childrenList.length!==0" 
              :parent-item = "item" :key="item.moduleId" >
            </side-menu-item>

            <menu-item v-else :key = "item.moduleId" :name = "item.moduleName">
              <span>{{ item.moduleName }}</span>
            </menu-item>
        </template>
    </Menu>
</template>
<script>
import sideMenuItem from '@/views/SideMenuItem.vue'
    export default {
        data () {
            return {
                menuData:[]
            }
        },
        created() {
          this.getMenuData()
        },
        components: {
          sideMenuItem
        },
        methods: {
          getMenuData() {
             var that = this;
             this.$api.post("Module/GetModules", null,r => {
                console.log(r.data);
                this.menuData = r.data;
             });
          }
        }
    }
</script>
