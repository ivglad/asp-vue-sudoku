<script setup>
import axios from "axios"
import { ref, reactive, computed, watch } from "vue"

import ButtonComponent from "@/components/UI/Button.vue";

const cells = ref()
cells.value = Array.apply(null, { length: 25 }).map((x, index) => {
  return {
    id: index,
    number: "*",
  }
}).sort(() => Math.random() - 0.5)

const setGridArray = async (fileGrid) => {
  cells.value = Array.apply(null, { length: cells.value.length }).map((x, index) => {
    return {
      id: index,
      number: "*",
    }
  }).sort(() => Math.random() - 0.5)

  await new Promise(resolve => setTimeout(resolve, 350))

  let valueIndex = 0
  fileGrid.forEach((row) => {
    row.forEach((value) => {
      cells.value[valueIndex] = {
        id: valueIndex,
        number: value === 0 ? "*" : value,
      }
      valueIndex++
    })
  })
}

const shuffle = () => {
  cells.sort(() => Math.random() - 0.5);
}

const error = ref("")

const selectedFile = ref()
const selectedFileName = ref()
const selectFile = (e) => {
  const targetFiles = e.target.files
  if (!targetFiles.length) return
  const file = e.target.files[0]

  error.value = ""

  if (file.name.slice(-4) !== ".txt") {
    error.value = `Некорректный файл для загрузки - "${file.name}"`
    selectedFileName.value = null
    selectedFile.value = null
    uploadedFileName.value = null
    return
  }

  selectedFileName.value = file.name
  selectedFile.value = file
}

const uploadedFileName = ref()
const uploadFile = async () => {
  let formData = new FormData()
  formData.append('file', selectedFile.value)
  try {
    const response = await axios.post('/api/uploadFile', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      }
    )
    uploadedFileName.value = response.data
    error.value = ""
  } catch(e) {
    error.value = `Ошибка при отправке файла`
    console.log(`ERROR. Ошибка при отправке файла: ${e.message}`)
  }
}

const uploadedFileGrid = ref()
const showFileGrid = async () => {
  try {
    const response = await axios.post('/api/showFileGrid', uploadedFileName.value, {
        headers: {
          'Content-Type': 'text/plain'
        }
      }
    )
    uploadedFileGrid.value = response.data
    await setGridArray(uploadedFileGrid.value)
    error.value = ""
  } catch(e) {
    error.value = `Ошибка получения данных файла`
    console.log(`ERROR. Ошибка получения данных файла: ${e.message}`)
  }
}

const handleFileGrid = ref()
const sudokuHandle = async () => {
  try {
    const response = await axios.post('/api/sudokuHandle', uploadedFileName.value, {
        headers: {
          'Content-Type': 'text/plain'
        }
      }
    )
    handleFileGrid.value = response.data
    await setGridArray(handleFileGrid.value)
    checkNullCells()
  } catch(e) {
    error.value = `Ошибка обработки файла`
    console.log(`ERROR. Ошибка обработки файла: ${e.message}`)
  }
}

const checkNullCells = () => {
  const nullCellsTriger = cells.value.some((cell) => {
    if (cell.number === "*") {
      error.value = `Нет решения для данной комбинации судоку`
      return true
    }
  })
  if (!nullCellsTriger) error.value = ""
}

</script>

<template>
  <div class="content">
    <h1>СУДОКУ</h1>
    <h5>Для получения результата заполните текстовый файл (.txt) в формате - 5 строк по 5 символов (в соответствии с общим видом судоку 5х5) и отправьте его на сервер. Вы можете в произвольном порядке заполнить цифры или заменить их на *.</h5>
    <div class="send-file">
      <button-component b-type="select file"
                        b-title="Выбрать файл"
                        :b-active="true"
                        @change="selectFile">
      </button-component>
      <button-component :b-title="selectedFile ? 'Загрузить - ' + selectedFileName : 'Файл не выбран'"
                        :b-active="selectedFile ? true : false"
                        @click="uploadFile">
      </button-component>
    </div>

    <transition-group tag="div" 
                      name="cell" 
                      class="sudoku-grid">
      <div v-for="cell in cells" 
          :key="cell.id" 
          class="cell">
        {{ cell.number }}
      </div>
    </transition-group>

    <div class="file-actions">
      <button-component b-title="Показать"
                        :b-active="uploadedFileName ? true : false"
                        @click="showFileGrid">
      </button-component>
      <button-component b-title="Решить"
                        :b-active="uploadedFileName ? true : false"
                        @click="sudokuHandle">
      </button-component>
    </div>
    <div class="error">
      <transition name="list-fade">
        <h5 v-if="error">{{ error }}</h5>
      </transition>
    </div>
  </div>
</template>

<style lang="sass" scoped>
@use '@/assets/styles' as *

.content
  width: 100%
  max-width: 600px
  min-width: 200px
  h1
    margin-bottom: $m-offset
    font-weight: bold
  h5
    display: flex
    margin-top: 0
    font-weight: normal
    overflow-wrap: anywhere
.send-file, .file-actions
    display: flex
.sudoku-grid
  display: grid
  grid-template-rows: repeat(5, $font-size-title)
  grid-template-columns: repeat(5, $font-size-title)
  margin: 0 0 $m-offset 0
  .cell
    display: flex
    justify-content: center
    align-items: center
  .cell-move
    @include transition-enter(all, 0.35s, cubic-bezier(.25,0,0,1), 0.0s)
.error
  color: $color-attention
  font-weight: normal
  min-height: 80px
</style>
