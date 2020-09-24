<template>
  <v-data-table dense 
    :headers="headers"
    :items= "points" 
    item-key="name"
    class="elevation-1">
</v-data-table>
</template>

<script>
  export default {
    props: {
        points: Array
    },
    data: () => ({

     
      headers: [
        {
          text: 'Номер',
          align: 'start',
          sortable: false,
          value: 'number',
        },
        { text: 'автор', value: 'author' },
        { text: 'x', value: 'y' },
        { text: 'y', value: 'y' },
      ],
    }),
  }
</script>
