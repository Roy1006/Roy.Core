<template>
  <div class="home">
     <div v-for="i in list"  :key="i.functionId">
       <ul class="entry">
         <li>
           <a>
             {{i.functionName}}
           </a>
         </li>
       </ul>
     </div>
  </div>
</template>

<script>
// @ is an alias to /src
export default {
  name: 'home',
  components: {},
  data(){
    return {
      list:[]
    }
  },
  created(){
    this.getData()
  },
  methods:{
    getData(){
      this.$api.get('/Function/GetSingleFunction',null,r=>{
        this.list= r
      })
    }
  },
  watch: {
        '$route'(to, from) {
            this.list=[]
            this.getData()
        }
    }
}
</script>
