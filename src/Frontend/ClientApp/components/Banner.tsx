import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface State {
    images: string[],
    currentImageId: number,
    currentImage: string,
    intervalId: number
}

export class Banner extends React.Component<{}, State> {
    constructor(props: any) {
        super(props);
        this.state = {
          images: ["http://barkpost-assets.s3.amazonaws.com/wp-content/uploads/2013/11/plainDoge.jpg", "https://i.pinimg.com/736x/36/8d/fe/368dfe1131382533c9c05b13abf59845.jpg"],
          currentImageId: 0,
          currentImage: "",
          intervalId: 0
        };
      }

      componentDidMount() {
        const interval = setInterval(this.autoChange.bind(this), 3000);
        this.setState({
            currentImage: this.getImage(0),
            intervalId: interval
        })
      }

      componentWillUnmount() {
          clearInterval(this.state.intervalId);
      }


      handleClick() {
        if(this.state.currentImageId === 0) {
            this.setState({ currentImageId: 1, currentImage: this.getImage(1)});
        } else {
            this.setState({ currentImageId: 0, currentImage: this.getImage(0)});
        }
      }

      autoChange() {
          this.handleClick();
      }

      getImage(id: number) {
          return this.state.images[id];
      }
    
    public render() {
        return <div> 
            <img src={this.state.currentImage}/>
            <button onClick={() => this.handleClick()}>Click me!</button>
        </div>;
    }
}
