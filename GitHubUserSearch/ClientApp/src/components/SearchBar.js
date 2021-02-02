import React, { Component } from 'react';
import axios from 'axios';
import PageNavigation from './PageNavigation';
import Loader from '../loader.gif';
import '../Search.css';
export class SearchBar extends Component {
    static displayName = SearchBar.name;

	constructor(props) {
		super(props);

		this.state = {
			query: '',
			results: {},
			loading: false,
			message: '',
			totalResults: 0,
			totalPages: 0,
			currentPageNo: 0,
		};

		this.cancel = '';
	}

	getPageCount = (total, denominator) => {
		const divisible = 0 === total % denominator;
		const valueToBeAdded = divisible ? 0 : 1;
		return Math.floor(total / denominator) + valueToBeAdded;
	};

	fetchSearchResults = (updatedPageNo = '', query) => {
		const pageNumber = updatedPageNo ? updatedPageNo : '1';
		const searchUrl = `https://github-name-search.herokuapp.com/api/user/name/${query}/page/${pageNumber}`;

		if (this.cancel) {
			this.cancel.cancel();
		}

		this.cancel = axios.CancelToken.source();

		axios.get(searchUrl, {
			cancelToken: this.cancel.token
		})
			.then(response => {
				const total = response.data.Users.length;
				const totalPagesCount = response.data.TotalPageNumbers;
				const resultNotFoundMsg = !response.data.Users.length
					? 'There are no more search results. Please try a new search'
					: '';
				this.setState({
					results: response.data.Users,
					message: resultNotFoundMsg,
					totalResults: total,
					totalPages: totalPagesCount,
					currentPageNo: updatedPageNo,
					loading: false
				})
			})
			.catch(error => {
				if (error.message !== undefined) {
					this.setState({
						loading: false,
						message: 'Failed to fetch the data. Please check network'
					})
				}
			})
	};

	handleOnInputChange = (event) => {
		const query = event.target.value;
		if (!query) {
			this.setState({ query, results: {}, message: '', totalPages: 0, totalResults: 0 });
		}
		else {
			this.setState({ query, loading: true, message: '' }, () => {
				this.fetchSearchResults(1, query);
			});
		}
	};

	handlePageClick = (type, event) => {
		event.preventDefault();
		const updatePageNo = 'prev' === type
			? this.state.currentPageNo - 1
			: this.state.currentPageNo + 1;

		if (!this.state.loading) {
			this.setState({ loading: true, message: '' }, () => {
				this.fetchSearchResults(updatePageNo, this.state.query);
			});
		}
	};

	renderSearchResults = () => {
		const { results } = this.state;

		if (Object.keys(results).length && results.length) {
			return (
				<div className="container">
					{ results.map(result => {
						return (
							<div className="row resultRow">
								<div className="col-md-4">
									<a key={result.id} href={result.html_url} className="">
										<h6 className="image-username">{result.login}</h6>
										<div className="image-wrapper">
											<img className="image" src={result.avatar_url} alt={`${result.login} image`} />
										</div>
									</a>
								</div>
								<div className="col-md-8">
									<div className="row">
										<span class="attribute-label">Full Name:</span>&nbsp;{result.name}
									</div>									
									<div className="row">
										<span class="attribute-label">Current Location:</span>&nbsp;{result.location}
									</div>
									<div className="row">
										<span class="attribute-label">Email:</span>&nbsp;{result.email}
									</div>
									<div className="row">
										<span class="attribute-label">Number of Repos:</span>&nbsp;{result.public_repos}
									</div>
									<div className="row">
										<span class="attribute-label">Profile Created:</span>&nbsp;{result.created_at}
									</div>
									<div className="row">
										<span class="attribute-label">Last Updated:</span>&nbsp;{result.updated_at}
									</div>
									<div className="row">
										<span class="attribute-label">Is Hireable:</span>&nbsp;{result.hireable}
									</div>
								</div>
							</div>
						)
					})}
				</div>
			)
		}
	};

	render() {
		const { query, loading, message, currentPageNo, totalPages } = this.state;

		const showPrevLink = 1 < currentPageNo;
		const showNextLink = totalPages > currentPageNo;

		return (
			<div className="container">
				<h2 className="heading">GitHub User Search</h2>
				<label className="search-label" htmlFor="search-input">
					<input
						type="text"
						name="query"
						value={query}
						id="search-input"
						placeholder="Search..."
						onChange={this.handleOnInputChange}
					/>
					<i className="fa fa-search search-icon" aria-hidden="true" />
				</label>

				{message && <p className="message">{message}</p>}

				<img src={Loader} className={`search-loading ${loading ? 'show' : 'hide'}`} alt="loader" />

				{this.renderSearchResults()}

				<PageNavigation
					loading={loading}
					showPrevLink={showPrevLink}
					showNextLink={showNextLink}
					handlePrevClick={(event) => this.handlePageClick('prev', event)}
					handleNextClick={(event) => this.handlePageClick('next', event)}
				/>
			</div>
		)
	}
}