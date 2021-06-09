<template>
  <div>
      <v-row>
        <v-col>
            <BuscadorPedidos/>
        </v-col>
        <v-col>
            <GestorPedidos/>
        </v-col>
      </v-row>
  </div>

</template>

<script>
  import BuscadorPedidos from '../components/BuscadorPedidos'
  import GestorPedidos from '../components/GestorPedidos'
  export default {
    name: 'App',
    components:{
          BuscadorPedidos,
          GestorPedidos
      },
    data: () => ({
     
    }),
  };
</script>