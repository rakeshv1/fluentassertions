﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentAssertions.Assertions
{
    public class GenericDictionaryAssertions<TKey, TValue>
    {
        protected internal GenericDictionaryAssertions(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary != null)
            {
                Subject = dictionary;
            }
        }

        #region BeNull

        /// <summary>
        /// Gets the object which value is being asserted.
        /// </summary>
        public IDictionary<TKey, TValue> Subject { get; private set; }

        /// <summary>
        /// Asserts that the current dictionary has not been initialized yet with an actual dictionary.
        /// </summary>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> BeNull()
        {
            return BeNull(string.Empty);
        }

        /// <summary>
        /// Asserts that the current dictionary has not been initialized yet with an actual dictionary.
        /// </summary>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> BeNull(string reason, params object[] reasonArgs)
        {
            if (!ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary to be <null>{2}, but found {1}.", null, Subject, reason, reasonArgs);
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        /// <summary>
        /// Asserts that the current dictionary has been initialized with an actual dictionary.
        /// </summary>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotBeNull()
        {
            return NotBeNull("");
        }

        /// <summary>
        /// Asserts that the current dictionary has been initialized with an actual dictionary.
        /// </summary>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotBeNull(string reason, params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary not to be <null>{2}.", null, Subject, reason, reasonArgs);
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion

        #region HaveCount

        /// <summary>
        /// Asserts that the number of items in the dictionary matches the supplied <paramref name="expected" /> amount.
        /// </summary>
        /// <param name="expected">
        /// The expected amount.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> HaveCount(int expected)
        {
            return HaveCount(expected, string.Empty);
        }

        /// <summary>
        /// Asserts that the number of items in the dictionary matches the supplied <paramref name="expected" /> amount.
        /// </summary>
        /// <param name="expected">
        /// The expected amount.
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> HaveCount(int expected, string reason,
            params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected {0} item(s){2}, but found {1}.", expected, Subject, reason, reasonArgs);
            }

            int actualCount = Subject.Count;

            Execute.Verify(() => actualCount == expected, "Expected {0} item(s){2}, but found {1}.",
                expected, actualCount, reason, reasonArgs);

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        /// <summary>
        /// Asserts that the number of items in the dictionary matches a condition stated by a predicate.
        /// </summary>
        /// <param name="countPredicate">
        /// The predicate which must be statisfied by the amount of items
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> HaveCount(Expression<Func<int, bool>> countPredicate)
        {
            return HaveCount(countPredicate, String.Empty);
        }

        /// <summary>
        /// Asserts that the number of items in the dictionary matches a condition stated by a predicate.
        /// </summary>
        /// <param name="countPredicate">
        /// The predicate which must be statisfied by the amount of items
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> HaveCount(Expression<Func<int, bool>> countPredicate,
            string reason, params object[] reasonArgs)
        {
            if (countPredicate == null)
            {
                throw new NullReferenceException("Cannot compare dictionary count against a <null> predicate.");
            }

            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected {0} items{2}, but found {1}.", countPredicate.Body, Subject, reason,
                    reasonArgs);
            }

            Func<int, bool> compiledPredicate = countPredicate.Compile();

            int actualCount = Subject.Count;

            if (!compiledPredicate(actualCount))
            {
                Execute.Fail(
                    "Expected dictionary {0} to have a count " + countPredicate.Body + "{2}, but count is {1}.", Subject,
                    actualCount, reason, reasonArgs);
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion

        #region BeEmpty

        /// <summary>
        /// Asserts that the dictionary does not contain any items.
        /// </summary>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> BeEmpty()
        {
            return BeEmpty(String.Empty);
        }

        /// <summary>
        /// Asserts that the dictionary does not contain any items.
        /// </summary>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> BeEmpty(string reason, params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary to be empty{2}, but found {1}.", "", Subject, reason, reasonArgs);
            }

            Execute.Verify(() => !Subject.Any(), "Expected no items{2}, but found {1}.",
                null, Subject.Count(), reason, reasonArgs);

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        /// <summary>
        /// Asserts that the dictionary contains at least 1 item.
        /// </summary>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotBeEmpty()
        {
            return NotBeEmpty(String.Empty);
        }

        /// <summary>
        /// Asserts that the dictionary contains at least 1 item.
        /// </summary>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotBeEmpty(string reason,
            params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary not to be empty{2}, but found {1}.", "", Subject, reason,
                    reasonArgs);
            }

            Execute.Verify(() => Subject.Any(), "Expected one or more items{2}.",
                null, 0, reason, reasonArgs);

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion

        #region Equal

        /// <summary>
        /// Asserts that the current dictionary contains all the same key-value pairs as the
        /// specified <paramref name="expected"/> dictionary. Keys and values are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected dictionary
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> Equal(IDictionary<TKey, TValue> expected)
        {
            return Equal(expected, String.Empty);
        }

        /// <summary>
        /// Asserts that the current dictionary contains all the same key-value pairs as the
        /// specified <paramref name="expected"/> dictionary. Keys and values are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected dictionary
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> Equal(IDictionary<TKey, TValue> expected, string reason, params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary to be equal to {0}{2}, but found {1}.", expected, Subject, reason,
                    reasonArgs);
            }

            if (expected == null)
            {
                throw new ArgumentNullException("expected", "Cannot compare dictionary with <null>.");
            }

            IEnumerable<TKey> missingKeys = expected.Keys.Except(Subject.Keys);
            IEnumerable<TKey> additionalKeys = Subject.Keys.Except(expected.Keys);

            if (missingKeys.Any())
            {
                Execute.Fail("Expected dictionary to be equal to {0}{2}, but could not find keys {3}.", expected, Subject, reason,
                    reasonArgs, missingKeys);
            }
            
            if (additionalKeys.Any())
            {
                Execute.Fail("Expected dictionary to be equal to {0}{2}, but found additional keys {3}.", expected, Subject, reason,
                    reasonArgs, additionalKeys);
            }

            foreach (var key in expected.Keys)
            {
                Execute.Verification
                    .ForCondition(Subject[key].Equals(expected[key]))
                    .BecauseOf(reason, reasonArgs)
                    .FailWith("Expected " + Verification.SubjectNameOr("dictionary") +
                            " to be equal to {1}{0}, but {2} differs at key " + key + ".", expected, Subject);
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        /// <summary>
        /// Asserts the current dictionary not to contain all the same key-value pairs as the
        /// specified <paramref name="unexpected"/> dictionary. Keys and values are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="unexpected">
        /// The unexpected dictionary
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotEqual(IDictionary<TKey, TValue> unexpected)
        {
            return NotEqual(unexpected, String.Empty);
        }

        /// <summary>
        /// Asserts the current dictionary not to contain all the same key-value pairs as the
        /// specified <paramref name="unexpected"/> dictionary. Keys and values are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="unexpected">
        /// The unexpected dictionary
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotEqual(IDictionary<TKey, TValue> unexpected, string reason,
            params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionaries not to be equal{2}, but found {1}.", unexpected, Subject, reason,
                    reasonArgs);
            }

            if (unexpected == null)
            {
                throw new ArgumentNullException("unexpected", "Cannot compare dictionary with <null>.");
            }

            IEnumerable<TKey> missingKeys = unexpected.Keys.Except(Subject.Keys);
            IEnumerable<TKey> additionalKeys = Subject.Keys.Except(unexpected.Keys);

            bool foundDifference = missingKeys.Any()
                || additionalKeys.Any()
                || (Subject.Keys.Any(key => !Subject[key].Equals(unexpected[key])));

            if (!foundDifference)
            {
                Execute.Fail("Did not expect dictionaries {0} and {1} to be equal{2}.", unexpected, Subject, reason,
                    reasonArgs);
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion

        #region ContainKey

        /// <summary>
        /// Asserts that the dictionary contains the specified key. Keys are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected key
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> ContainKey(TKey expected)
        {
            return ContainKey(expected, String.Empty);
        }

        /// <summary>
        /// Asserts that the dictionary contains the specified key. Keys are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected key
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> ContainKey(TKey expected, string reason,
            params object[] reasonArgs)
        {
            return ContainKeys(new[] { expected }, reason, reasonArgs);
        }

        /// <summary>
        /// Asserts that the dictionary contains all of the specified keys. Keys are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected keys
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> ContainKeys(params TKey[] expected)
        {
            return ContainKeys(expected, String.Empty);
        }

        /// <summary>
        /// Asserts that the dictionary contains all of the specified keys. Keys are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected keys
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> ContainKeys(IEnumerable<TKey> expected, string reason,
            params object[] reasonArgs)
        {
            if (expected == null)
            {
                throw new NullReferenceException("Cannot verify key containment against a <null> collection of keys");
            }

            TKey[] expectedKeys = expected.ToArray();

            if (!expectedKeys.Any())
            {
                throw new ArgumentException("Cannot verify key containment against an empty dictionary");
            }

            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary to contain keys {0}, but found {1}.", expected, Subject, reason,
                    reasonArgs);
            }

            var missingKeys = expectedKeys.Except(Subject.Keys);
            if (missingKeys.Any())
            {
                if (expectedKeys.Count() > 1)
                {
                    Execute.Verification
                        .BecauseOf(reason, reasonArgs)
                        .FailWith("Expected dictionary {1} to contain key {2}{0}, but could not find {3}.", Subject,
                            expected, missingKeys);
                }
                else
                {
                    Execute.Verification
                        .BecauseOf(reason, reasonArgs)
                        .FailWith("Expected dictionary {1} to contain key {2}{0}.", Subject,
                            expected.Cast<object>().First());
                }
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion

        #region NotContainKey

        /// <summary>
        /// Asserts that the current dictionary does not contain the specified <paramref name="unexpected" /> key.
        /// Keys are compared using their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The unexpected key
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotContainKey(TKey unexpected)
        {
            return NotContainKey(unexpected, String.Empty);
        }

        /// <summary>
        /// Asserts that the current dictionary does not contain the specified <paramref name="unexpected" /> key.
        /// Keys are compared using their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The unexpected key
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotContainKey(TKey unexpected, string reason,
            params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary not to contain key {0}{2}, but found {1}.", unexpected, Subject,
                    reason, reasonArgs);
            }

            if (Subject.ContainsKey(unexpected))
            {
                Execute.Fail("Dictionary {1} should not contain key {0}{2}, but found it anyhow.",
                    unexpected, Subject, reason, reasonArgs);
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion

        #region ContainValue

        /// <summary>
        /// Asserts that the dictionary contains the specified value. Values are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected value
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> ContainValue(TValue expected)
        {
            return ContainValue(expected, String.Empty);
        }

        /// <summary>
        /// Asserts that the dictionary contains the specified value. Values are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected value
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> ContainValue(TValue expected, string reason,
            params object[] reasonArgs)
        {
            return ContainValues(new[] { expected }, reason, reasonArgs);
        }

        /// <summary>
        /// Asserts that the dictionary contains all of the specified values. Values are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected values
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> ContainValues(params TValue[] expected)
        {
            return ContainValues(expected, String.Empty);
        }

        /// <summary>
        /// Asserts that the dictionary contains all of the specified values. Values are compared using
        /// their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The expected values
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> ContainValues(IEnumerable<TValue> expected, string reason,
            params object[] reasonArgs)
        {
            if (expected == null)
            {
                throw new NullReferenceException("Cannot verify value containment against a <null> collection of values");
            }

            TValue[] expectedValues = expected.ToArray();

            if (!expectedValues.Any())
            {
                throw new ArgumentException("Cannot verify value containment against an empty dictionary");
            }

            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary to contain value {0}, but found {1}.", expected, Subject, reason,
                    reasonArgs);
            }

            var missingValues = expectedValues.Except(Subject.Values);
            if (missingValues.Any())
            {
                if (expectedValues.Count() > 1)
                {
                    Execute.Verification
                        .BecauseOf(reason, reasonArgs)
                        .FailWith("Expected dictionary {1} to contain value {2}{0}, but could not find {3}.", Subject,
                            expected, missingValues);
                }
                else
                {
                    Execute.Verification
                        .BecauseOf(reason, reasonArgs)
                        .FailWith("Expected dictionary {1} to contain value {2}{0}.", Subject,
                            expected.Cast<object>().First());
                }
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion

        #region NotContainValue

        /// <summary>
        /// Asserts that the current dictionary does not contain the specified <paramref name="unexpected" /> value.
        /// Values are compared using their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The unexpected value
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotContainValue(TValue unexpected)
        {
            return NotContainValue(unexpected, String.Empty);
        }

        /// <summary>
        /// Asserts that the current dictionary does not contain the specified <paramref name="unexpected" /> value.
        /// Values are compared using their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">
        /// The unexpected value
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> NotContainValue(TValue unexpected, string reason,
            params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary not to contain value {0}{2}, but found {1}.", unexpected, Subject,
                    reason, reasonArgs);
            }

            if (Subject.Values.Contains(unexpected))
            {
                Execute.Fail("Dictionary {1} should not contain value {0}{2}, but found it anyhow.",
                    unexpected, Subject, reason, reasonArgs);
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion

        #region Contain

        /// <summary>
        /// Asserts that the current dictionary has the specified <paramref name="value" /> for the supplied <paramref
        /// name="key" />. Values are compared using their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="key">
        /// The key for which to validate the value
        /// </param>
        /// <param name="value">
        /// The value to validate
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> Contain(TKey key, TValue value)
        {
            return Contain(key, value, String.Empty);
        }

        /// <summary>
        /// Asserts that the current dictionary has the specified <paramref name="value" /> for the supplied <paramref
        /// name="key" />. Values are compared using their <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="key">
        /// The key for which to validate the value
        /// </param>
        /// <param name="value">
        /// The value to validate
        /// </param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public AndConstraint<GenericDictionaryAssertions<TKey, TValue>> Contain(TKey key, TValue value, string reason,
            params object[] reasonArgs)
        {
            if (ReferenceEquals(Subject, null))
            {
                Execute.Fail("Expected dictionary to have value at key {0}{2}, but found {1}.", key, Subject,
                    reason, reasonArgs);
            }

            if (Subject.ContainsKey(key))
            {
                TValue actual = Subject[key];

                Execute.Verify(actual.Equals(value),
                    "Expected {0} at key " + key + "{2}, but found {1}.",
                    value, actual, reason, reasonArgs);
            }
            else
            {
                Execute.Fail("Expected {0} at key " + key + "{2}, but the key was not found.",
                    value, null, reason, reasonArgs);
            }

            return new AndConstraint<GenericDictionaryAssertions<TKey, TValue>>(this);
        }

        #endregion
    }
}