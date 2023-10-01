<script setup>
import { ref, reactive, computed, watch } from "vue"

const props = defineProps({
  bType: {
    type: String,
    default: "select",
    validator(value) {
      return ["select", "select file"].includes(value);
    },
  },
  bTitle: {
    type: [String, Number],
    default: "Кнопка",
  },
  bActive: {
    type: Boolean,
    default: true,
  },
  bSelectFocus: {
    type: Boolean,
    default: false,
  }
})

const mouseDown = ref(false);

const classes = computed(() => {
  return {
    "button-active": props.bActive && !mouseDown.value,
    "button-focus": props.bActive && props.bType === "select" && props.bSelectFocus,
    "button-inactive": !props.bActive || mouseDown.value,
    "button-inactive-mousedown": mouseDown.value
  }
})

const mainHandler = (e) => {
  if (!props.bActive) {
    e.stopImmediatePropagation();
  }
}

</script>



<template>
  <div class="button-component">
    <button v-if="bType === 'select'"
            class="button"
            :class="classes"
            @click="mainHandler"
            @mousedown="mouseDown = true"
            @mouseup="mouseDown = false">{{ bTitle }}
    </button>
    <label v-if="bType === 'select file'"
           for="file-input"
           class="select-file">
      <input type="file"
             name="file"
             id="file-input">
      <div class="button"
           :class="classes"
           @click="mainHandler"
           @mousedown="mouseDown = true"
           @mouseup="mouseDown = false">{{ bTitle }}
      </div>
 	  </label>
  </div>
</template>



<style lang="sass" scoped>
@use '@/assets/styles' as *

.button-component
  display: flex
  margin: 0 $m-offset $m-offset 0
.button
  @include font-base-button(0.4 * $s-offset, 1.0 * $s-offset)
  @include transition-leave
  height: $font-size-title
  min-width: $font-size-title
  max-width: calc($font-size-title * 10)
  box-sizing: border-box
  color: $background-base
  background-color: $e-base
  border-width: 0
  border-radius: $button-radius
  cursor: pointer
  user-select: none
  text-transform: uppercase
  align-self: center
  text-align: center
  text-overflow: ellipsis
  outline: none
  overflow: hidden
  @include hover
    @include transition-enter
    background-color: $color-base-active
    box-shadow: 0 0 $xs-offset $color-base-active
.button-active
  @include hover
    @include transition-enter
.button-focus
  background-color: $e-base-active
  box-shadow: 0 0 $xs-offset $e-base-active
.button-remove
  @include font-base-button(0.4 * $s-offset, 1.0 * $s-offset)
  @include transition-leave
  height: $font-size-title
  min-width: $font-size-title
  box-sizing: border-box
  color: $t-attention
  background-color: transparent
  border-width: 0
  cursor: pointer
  user-select: none
  text-transform: uppercase
  align-self: center
  text-align: center
  outline: none
  @include hover
    @include transition-enter
    text-shadow: 0 0 $xs-offset $t-attention
.button-remove-active
  @include hover
    @include transition-enter
    text-shadow: 0 0 0 transparent
.button-inactive
  background-color: $e-inactive-2
  text-shadow: 0 0 0 transparent
  cursor: default
  @include hover
    background-color: $e-inactive-2
    text-shadow: 0 0 0 transparent
    box-shadow: 0 0 0 transparent
  &:focus
    background-color: $e-inactive-2
    text-shadow: 0 0 0 transparent
    box-shadow: 0 0 0 transparent
.button-inactive-mousedown
  &::after
    content: ""
    position: fixed
    top: 0
    left: 0
    width: 100vw
    height: 100vh
    z-index: 1
.select-file
  display: flex
  position: relative
  input
    position: absolute
    opacity: 0
    width: 0
    height: 0
  div
    line-height: 2.2rem
</style>