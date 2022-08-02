
// ---------- Components ---------- //

function Button() {
    return (<button>Press Me!</button>);
}


// ---------- Main App ---------- //
var App = React.createClass({
    render: function() {
        return (
            <div>
                <h1>Hello world</h1>
                <Button />
            </div>
            );
    }
});

React.render(<App />, document.getElementById('root'));