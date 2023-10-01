<template>
  <header>
  </header>
  <main>
    <section>
      <router-view />
    </section>
  </main>
</template>


<script setup>

</script>


<style lang="sass">
@use '@/assets/styles' as *

html
  width: 100vw
  height: 100vh
  font-size: 62.5%
  line-height: 100%
  word-break: normal

body
  @include font-base
  width: 100vw
  height: 100vh
  margin: 0
  padding: 0
  box-sizing: border-box
  background-color: $background-base

  #app
    display: flex
    flex-direction: column
    position: relative
    width: 100%
    height: 100%
    overflow-x: hidden

    header
      display: flex
      align-content: center
      justify-content: center
      position: absolute
      width: 100%
      // top: 100%
      bottom: 0
      z-index: 100

    main
      display: flex
      width: 100%
      height: 100%
      min-width: 400px
      padding: $d-offset
      position: relative
      overflow: hidden
      align-content: center
      justify-content: center
      section
        display: flex
        align-content: center
        justify-content: center
        flex-wrap: wrap
      .content
        display: flex
        flex-direction: column
        min-height: 50%
        // max-width: 54%
</style>
