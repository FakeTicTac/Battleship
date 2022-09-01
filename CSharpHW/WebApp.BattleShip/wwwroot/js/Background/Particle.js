

class Particle
{
    constructor(canvas, progress){
        let random = Math.random()
        this.progress = 0;
        this.canvas = canvas;

        this.x = ($(window).width()/2)  + (Math.random()*500 - Math.random()*500)
        this.y = ($(window).height()/2) + (Math.random()*500 - Math.random()*500)

        this.w = $(window).width()
        this.h = $(window).height()
        this.radius = random > .2 ? Math.random()*1 : Math.random()*3
        this.color  = random > .2 ? "#FFCB" : "#FFCB9A"
        this.radius = random > .8 ? Math.random()*2 : this.radius
        this.color  = random > .8 ? "#7DFFF2" : this.color

        // this.color  = random > .1 ? "#ffae00" : "#f0ff00" // Alien
        this.variantx1 = Math.random()*600
        this.variantx2 = Math.random()*800
        this.varianty1 = Math.random()*200
        this.varianty2 = Math.random()*240
    }

    render(){
        this.canvas.beginPath();
        this.canvas.arc(this.x, this.y, this.radius, 0, 2 * Math.PI);
        this.canvas.lineWidth = 2;
        this.canvas.fillStyle = this.color;
        this.canvas.fill();
        this.canvas.closePath();
    }

    move(){
        this.x += (Math.sin(this.progress/this.variantx1)*Math.cos(this.progress/this.variantx2));
        this.y += (Math.sin(this.progress/this.varianty1)*Math.cos(this.progress/this.varianty2));


        if(this.x < 0 || this.x > this.w - this.radius){
            return false
        }

        if(this.y < 0 || this.y > this.h - this.radius){
            return false
        }
        this.render()
        this.progress++
        return true
    }
}
