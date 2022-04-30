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
    <!--<div align="center">
      <b-col lg="6" class="my-1">
        <b-form-group class="mb-0">
          <b-input-group size="sm">
            <b-form-input id="filter-input"
                          v-model="filter"
                          type="search"
                          placeholder="Type to Search"></b-form-input>

            <b-input-group-append>
              <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
            </b-input-group-append>
          </b-input-group>
        </b-form-group>
      </b-col>

      <b-col lg="6" class="my-1">
        <b-form-group label="Columns to filter:"
                      label-cols-sm="3"
                      label-align-sm="right"
                      label-size="sm"
                      class="mb-0"
                      v-slot="{ ariaDescribedby }">
          <b-form-checkbox-group v-model="filterOn"
                                 :aria-describedby="ariaDescribedby"
                                 class="mt-1">
            <b-form-checkbox value="Id">Id</b-form-checkbox>
            <b-form-checkbox value="Gender">Gender</b-form-checkbox>
            <b-form-checkbox value="AgeBracket">Age Bracket</b-form-checkbox>
            <b-form-checkbox value="MunicipalityCode">Municipality Code</b-form-checkbox>
            <b-form-checkbox value="MunicipalityName">Municipality Name</b-form-checkbox>
            <b-form-checkbox value="ConfirmationDate">Confirmation Date</b-form-checkbox>
          </b-form-checkbox-group>
        </b-form-group>
      </b-col>

      <b-col sm="5" md="6" class="my-1">
        <b-form-group label="Items per page"
                      label-for="per-page-select"
                      label-cols-sm="6"
                      label-cols-md="4"
                      label-cols-lg="3"
                      label-align-sm="right"
                      label-size="sm"
                      class="mb-0">
          <b-form-select id="per-page-select"
                         v-model="perPage"
                         :options="pageOptions"
                         width="50px"
                         size="sm"></b-form-select>
        </b-form-group>
      </b-col>

      <b-col sm="7" md="6" class="my-1">
        <b-pagination v-model="currentPage"
                      :total-rows="totalRows"
                      :per-page="perPage"
                      align="fill"
                      size="sm"
                      class="my-0"></b-pagination>
      </b-col>

    </div>-->
    <!-- Main table element -->
    <!--<b-table :items="records"
             :fields="fields"
             :current-page="currentPage"
             :per-page="perPage"
             :filter="filter"
             :filter-included-fields="filterOn"
             stacked="md"
             show-empty
             small
             @filtered="onFiltered">
    </b-table>-->

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
        //const isUpdate = !!this.model.id;

        //if (isUpdate) {
        //  await api.update(this.model.id, this.model)
        //} else {
        //  await api.create(this.model)
        //}
      },
    }
  }
</script>
