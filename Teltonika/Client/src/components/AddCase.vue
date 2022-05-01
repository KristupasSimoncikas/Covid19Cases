<template>
  <b-container fluid>
    <b-alert :show="loading" variant="info">Loading...</b-alert>
    <div align="center">
      <b-col lg="3" align="center">
        <b-card :title="('New Covid-19 case record')">
          <form @submit.prevent="createRecord">
            <b-form-group label="Gender">
              <b-form-input type="text" v-model="model.Gender"></b-form-input>
            </b-form-group>
            <b-form-group label="Age bracket">
              <b-form-input rows="4" v-model="model.AgeBracket" type="text"></b-form-input>
            </b-form-group>
            <b-form-group label="Municipality code">
              <b-form-input rows="4" v-model="model.MunicipalityCode" type="text"></b-form-input>
            </b-form-group>
            <b-form-group label="Municipality name">
              <b-form-input rows="4" v-model="model.MunicipalityName" type="text"></b-form-input>
            </b-form-group>
            <b-form-group label="Confirmation date">
              <b-form-input rows="4" v-model="model.ConfirmationDate" type="datetime-local"></b-form-input>
            </b-form-group>
            <b-form-group label="Case code">
              <b-form-input rows="4" v-model="model.CaseCode" type="text"></b-form-input>
            </b-form-group>
            <b-form-group label="Coordinate X">
              <b-form-input rows="4" v-model="model.CoordinateX" type="text"></b-form-input>
            </b-form-group>
            <b-form-group label="Coordinate Y">
              <b-form-input rows="4" v-model="model.CoordinateY" type="text"></b-form-input>
            </b-form-group>
            <div>
              <b-btn type="submit" variant="success">Save Record</b-btn>
            </div>
          </form>
        </b-card>
      </b-col>
    </div>
  </b-container>
</template>

<script>
  import api from '@/Covid19CasesAPIService';

  export default {
    data() {
      return {
        loading: false,
        fields: [
          { key: 'Id', filterByFormatted: true },
          { key: 'Gender', filterByFormatted: true },
          { key: 'AgeBracket', filterByFormatted: true },
          { key: 'MunicipalityCode', filterByFormatted: true },
          { key: 'MunicipalityName', filterByFormatted: true },
          { key: 'ConfirmationDate', filterByFormatted: true },
        ],
        totalRows: 1,
        currentPage: 1,
        perPage: 10,
        pageOptions: [5, 10, 15, 20],
        filter: null,
        filterOn: ['Id', 'Gender', 'AgeBracket', 'MunicipalityCode', 'MunicipalityName', 'ConfirmationDate',],
        records: [],
        model: {}
      };
    },
    computed: {
      sortOptions() {
        // Create an options list from our fields
        return this.fields
          .filter(f => f.sortable)
          .map(f => {
            return { text: f.label, value: f.key }
          })
      }
    },
    mounted() {
      // Set the initial number of items
      this.totalRows = this.records.length
    },
    async created() {
      this.getAll()
    },
    methods: {
      onFiltered(filteredItems) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
        this.currentPage = 1
      },
      async getAll() {
        this.loading = true

        try {
          this.records = await api.getAll()
        } finally {
          this.loading = false
          this.totalRows = this.records.length
        }
      },
      async createRecord() {
        api.create(this.model)
        this.model = {}
      },
    }
  }
</script>
