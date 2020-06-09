$(document).ready(function () {
  const miniprofileTrigger = $('.miniprofile-trigger')
  const miniprofile = $('.miniprofile')
  console.log(miniprofile)

  miniprofileTrigger.on('click', () => {
    miniprofile.addClass('miniprofile-active')
  })

})