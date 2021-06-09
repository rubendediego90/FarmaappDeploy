<template >
    <v-row>
        <v-col>
            <BuscadorCaja/>
        </v-col>
        <v-col>
            <Carrito/>
        </v-col>
    </v-row>
</template>

<script>
  import BuscadorCaja from '../components/BuscadorCaja'
  import Carrito from '../components/Carrito'
export default {
    components:{
          BuscadorCaja,
          Carrito,
      },

}
</script>

