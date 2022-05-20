import React from 'react';

class RaceFormModify extends React.Component {

    constructor(props) {
        super(props);
        this.state = {id: '', distance: '', style: '', swimmersNumber: ''};

        //  this.handleChange = this.handleChange.bind(this);
        // this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleIdChange = (event) => {
        this.setState({id: event.target.value})
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
            id: this.state.id,
            distance: this.state.distance,
            style: this.state.style,
            swimmersNumber: this.state.swimmersNumber
        }
        console.log('A race was modified: ');
        console.log(race);
        this.props.modifyFunc(race);
        event.preventDefault();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Id:
                    <input type="text" value={this.state.id} onChange={this.handleIdChange}/>
                </label><br/>
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

                <input type="submit" value="Modify race"/>
            </form>
        );
    }
}

export default RaceFormModify;