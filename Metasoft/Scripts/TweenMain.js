// TweenMax.to(target, 0.5, { fill: "red" })
//TweenLite.to(target, 1, { rotation: 360, transformOrigin: "50% 50%" }); //percents
//TweenLite.to(target, 1, { rotation: 360, transformOrigin: "50px 50px" }); //pixels
//var t = TweenMax.staggerFrom(".menuitem",1,{scale:0,opacity:0,delay:0.3,ease:Back.easeOut},0.1)
//TweenMax.from(element, 1, { width: 0, ease: Power2.easeOut, delay: 1.5 });
//TweenLite.to(element, 1, { rotationY:"+=360", ease: Linear.easeNone });




function TweenHome() {
    TweenMax.from(".explaintitle", 1, { x: 600, opacity: 0, delay: 0.5 });             /* Title animation*/
    TweenMax.from(".explainText", 1, { x: 600, opacity: 0, delay: 0.7 });              /* Text animation*/
    TweenMax.from("#formlogodiv", 1, { width: 0, ease: Power2.easeOut, delay: 0.5 });  /* Logo animation*/
}
function IconOver(id) {
    //var target = "#" + id;
    //TweenMax.to(target, 0.2, { scale: 1.3 });
}
function IconOut(id) {
    //var target = "." + id;
    //TweenMax.to(target, 0.2, { scale: 1 })
}
function AnimateAdminMenuItem(litarget)   { TweenMax.staggerFrom("." + litarget, 1, { opacity: 0, delay: 0.05, ease: Elastic.easeOut }, 0.02)}
function AnimateLogo(target)              { TweenLite.to(target, 1, { rotationY: "+=360", ease: Linear.easeNone });}
function GiraClockWise(target)            { TweenLite.to(target, 1, { rotation: 360, transformOrigin: "center center", smoothOrigin: true, delay: 1 });}
function AnimateX(target) { TweenLite.to(target, 1, { rotationX: "+=360", ease: Linear.easeNone }); }
function AnimateY(target) { TweenLite.to(target, 1, { rotationY: "+=360", ease: Linear.easeIn }); }
function TweenIcon()                      { TweenMax.staggerFrom(".icon",1,{scale:1.1,opacity:0,delay:0.5,ease:Back.easeOut},0.2)}
function TweenAction(element)             { TweenLite.from(element, 1, {scale:1.3, ease: Linear.easeNone });}
function TweenFilterContainer()           { TweenMax.from(".filtercontainer", 1, { aplha:0, ease: Power2.easeOut, delay: 0.5 });}
function TweenDropBtn()  { TweenLite.to(".dropbtn", 1, { rotation: "+=360", scale: 1.1, transformOrigin: "center center", smoothOrigin: true, delay: 1 }); }

