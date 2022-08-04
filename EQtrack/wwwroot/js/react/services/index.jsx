import Main from './pages/Main.jsx';

// ---------- Main App ---------- //
var App = React.createClass({
    render: function() {
        return (
            <div>
                <Main />
            </div>
            );
    }
});

React.render(<App />, document.getElementById('root'));