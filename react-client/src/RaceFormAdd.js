import React from 'react';

class RaceFormAdd extends React.Component {

    constructor(props) {
        super(props);
        this.state = {distance: '', style: '', swimmersNumber: ''};

        //  this.handleChange = this.handleChange.bind(this);
        // this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleDistanceChange = (event) => {
        this.setState({distance: event.target.value})
    }

    handleStyleChange = (event) => {
        this.setState({style: event.target.value})
    }

    handleSwimmersNumberChange = (event) => {
        this.setState({swimmersNumber: event.target.value})
    }

    handleSubmit = (event) => {

        let race = {
            distance: this.state.distance,
            style: this.state.style,
            swimmersNumber: this.state.swimmersNumber
        }
        console.log('A race was submitted: ');
        console.log(race);
        this.props.addFunc(race);
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Distance:
                    <input type="text" value={this.state.distance} onChange={this.handleDistanceChange}/>
                </label><br/>
                <label>
                    Style:
                    <input type="text" value={this.state.style} onChange={this.handleStyleChange}/>
                </label><br/>
                <label>
                    SwimmersNumber:
                    <input type="text" value={this.state.swimmersNumber} onChange={this.handleSwimmersNumberChange}/>
                </label><br/>

                <input type="submit" value="Add race"/>
            </form>
        );
    }
}

export default RaceFormAdd;